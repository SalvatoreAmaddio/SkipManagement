using Backend.ExtensionMethods;
using FrontEnd.Converters;
using SkipManagement.Model;
using System.Globalization;

namespace SkipManagement.Converters
{
    public class FetchCity : AbstractFetchModel<City, City>
    {
        protected override string Sql => new City().Select().All().From().Where().This("id").Statement();

        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Record = (City?)value;
            para.Add(new("id", Record?.CityID));
            return (City?)Db?.Retrieve(Sql, para).FirstOrDefault();
        }
    }
}
