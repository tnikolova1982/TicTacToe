namespace TicTacToe.Services.Services
{
    using System.Collections.Generic;

    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Models.Search;

    public class SearchTableService : ISearchTableService
    {
        public IEnumerable<TTable> Search<TQuery, TTable>(TQuery model, ISearchable<TQuery, TTable> searchable) where TQuery : SearchQuery
        {
            ExtendSearchStringProperty(model);

            int count = searchable.CountResults(model);
            IEnumerable<TTable> result = null;

            if (count > model.Offset)
            {
                result = searchable.Search(model);
            }

            return result;
        }

        private void ExtendSearchStringProperty(dynamic model)
        {
            var properties = model.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.PropertyType.Name.ToLower() == "string")
                {
                    var val = (string)prop.GetValue(model, null);
                    if (!string.IsNullOrEmpty(val))
                    {
                        var trim = string.Format("%{0}%", val.Replace('*', '%').Replace(' ', '%')).Trim();
                        prop.SetValue(model, trim, null);
                    }
                }
            }
        }
    }
}
