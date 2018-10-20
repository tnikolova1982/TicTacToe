namespace TicTacToe.PostgreSqlData.Context
{
    using System;
    using System.Data;

    using Npgsql;
    using TicTacToe.Data.Context;


    public class Transaction : ITransaction
    {
        public Transaction(Context context)
        {
            Context = context;

            Context.Connection = new NpgsqlConnection(Context.ConnectionString);

            if (Context.Connection.State != ConnectionState.Open)
            {
                Context.Connection.Open();
                Context.Transaction = Context.Connection.BeginTransaction();
            }
        }

        public Transaction()
        {
        }

        protected Context Context { get; set; }


        public void Commit()
        {
            Context.Transaction.Commit();
        }

        public void Rollback()
        {
            Context.Transaction.Rollback();
        }

        public void Dispose()
        {
            Context.Transaction.Dispose();

            try
            {
                Context.Connection.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
