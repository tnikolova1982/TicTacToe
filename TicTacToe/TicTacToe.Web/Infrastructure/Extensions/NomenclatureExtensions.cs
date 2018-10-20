namespace TicTacToe.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TicTacToe.Models;

    public static class NomenclatureExtensions
    {
        public static IEnumerable<SelectListItem> AsSelectListItems(this IEnumerable<Nomenclature> nomenclatures)
        {
            return nomenclatures.Select(n => new SelectListItem { Value = n.Id.ToString(), Text = n.Name });
        }

        public static IEnumerable<KeyValuePair<string, string>> AsKeyValuePairs(this IEnumerable<Nomenclature> nomenclatures)
        {
            return nomenclatures.Select(n => new KeyValuePair<string, string>(n.Id.ToString(), n.Name));
        }

        public static List<KeyValuePair<string, string>> AddDropDownAll(this List<KeyValuePair<string, string>> list)
        {
            list.Insert(0, new KeyValuePair<string, string>(string.Empty, "---Всички---"));

            return list;
        }

        public static List<SelectListItem> AddEmptyDropDownOption(this List<SelectListItem> list)
        {
            list.Insert(0, new SelectListItem { Value = string.Empty, Text = "---Изберете---" });

            return list;
        }
    }
}