using System;

public class Orm : IDisposable
{
    private Database _db;

    public Orm(Database database)
    {
        _db = database;
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public void Write(string data)
    {
        try
        {
            if (!data.Equals("good write"))
            {
                throw new InvalidOperationException();
            }
            _db.Write(data);
        }
        catch (Exception e)
        {
            Database.lastData = data;
            throw;
        }
        finally
        {
            _db.Dispose();
        }
    }

    public bool WriteSafely(string data)
    {
        if (!data.Equals("good write"))
        {
            return false;
        }
        _db.Write(data);
        return true;
    }
}
