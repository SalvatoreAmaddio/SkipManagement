using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using SkipManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipManagement.Controller
{
    public class CityListController : AbstractFormListController<City>
    {
        public override AbstractClause InstantiateSearchQry() =>
        new City().Select().All().From();

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public override Task<IEnumerable<City>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(City model)
        {
        }
    }
}