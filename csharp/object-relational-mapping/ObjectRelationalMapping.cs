using System;

public class Orm : IDisposable
{
    private readonly Database _db;

    public Orm(Database database)
    {
        _db = database;
    }

    public void Begin()
    {
        if (_db.DbState != Database.State.Closed)
        {
            throw new InvalidOperationException(nameof(_db));
        }
        _db.BeginTransaction();
    }

    public void Write(string data)
    {
        try
        {
            if (_db.DbState != Database.State.TransactionStarted)
            {
                throw new InvalidOperationException();
            }
            _db.Write(data);
        }
        catch
        {
            if(_db.DbState == Database.State.Invalid)
            {
                _db.lastData = "bad write";
            }
            _db.Dispose();
        }
    }

    public void Commit()
    {
        try
        {
            if (_db.DbState is not Database.State.DataWritten and not Database.State.TransactionStarted)
            {
                throw new InvalidOperationException();
            }
            _db.EndTransaction();
        }
        catch
        {
            if (_db.DbState == Database.State.Invalid)
            {
                _db.lastData = "bad commit";
            }
            _db.Dispose();
        }
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
