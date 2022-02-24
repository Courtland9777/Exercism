using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

public class RestApi
{
    private readonly JArray Db;
    private readonly Dictionary<string, User> _dbUsers;

    public RestApi(string database)
    {
        Db = JArray.Parse(database);
        _dbUsers = new Dictionary<string, User>();
        if (database.Equals("[]"))
        {
            return;
        }

        foreach (var user in Db.Children<JObject>())
        {
            var parsedUser = ParseUser(user);
            _dbUsers.Add(parsedUser.name, parsedUser);
        }
    }

    private static User ParseUser(JObject user)
    {
        var owesSortedDictionary = new SortedDictionary<string, int>();
        foreach (var prop in ((JObject)user["owes"]).Properties())
        {
            owesSortedDictionary.Add(prop.Name,(int)prop.Value);
        }
        
        var owed_bySortedDictionary = new SortedDictionary<string, int>();
        foreach (var prop in ((JObject)user["owed_by"]).Properties())
        {
            owed_bySortedDictionary.Add(prop.Name, (int)prop.Value);
        }

        return new User()
        {
            name = user["name"].ToString(),
            owes = owesSortedDictionary,
            owed_by = owed_bySortedDictionary,
        };
    }

    public string Get(string url, string payload = null)
    {
        if (Db.Count == 0) return "[]";
        var list = JObject.Parse(payload).GetValue("users").Select(name => name.ToString()).ToList();

        return JsonConvert.SerializeObject(list.Count == 0 ?
            Db.ToObject<JToken[]>() :
            list.Select(x => _dbUsers[x]).ToArray());
    }

    public string Post(string url, string payload)
    {
        if (url.Equals("/add"))
        {
            User user = new()
            {
                name = JObject.Parse(payload).GetValue("user").ToString(),
                owes = new SortedDictionary<string, int>(),
                owed_by = new SortedDictionary<string, int>()
            };
            _dbUsers.Add(user.name, user);
            return JsonConvert.SerializeObject(user);
        }

        var iou = JsonConvert.DeserializeObject<Iou>(payload);
        var lender = _dbUsers[iou.lender];
        var borrower = _dbUsers[iou.borrower];
        lender.owed_by.Add(iou.borrower, iou.amount);
        borrower.owes.Add(iou.lender, iou.amount);

        var responseList = new List<User>
        {
            lender,
            borrower
        };

        foreach (var user in responseList)
        {
            foreach (var key in user.owed_by.Keys)
            {
                if (!user.owes.ContainsKey(key))
                {
                    continue;
                }

                var owe = user.owes[key];
                var owed = user.owed_by[key];

                if (owe == owed)
                {
                    user.owes.Remove(key);
                    user.owed_by.Remove(key);
                    break;
                }

                if (owe > owed)
                {
                    user.owes[key] = owe - owed;
                    user.owed_by.Remove(key);
                    break;
                }

                if (owe < owed)
                {
                    user.owed_by[key] = owed - owe;
                    user.owes.Remove(key);
                    break;
                }
            }
        }

        return JsonConvert.SerializeObject(responseList.OrderBy(n => n.name).ToArray());

    }
}

public class Iou
{
    public string lender { get; set; }
    public string borrower { get; set; }
    public int amount { get; set; }
}

public class User
{
    public string name { get; set; }
    public SortedDictionary<string, int> owes { get; set; }
    public SortedDictionary<string, int> owed_by { get; set; }

    public int balance => (owed_by.Count == 0 ? 0 : owed_by.Values.Sum()) - (owes.Count == 0 ? 0 : owes.Values.Sum());
}

