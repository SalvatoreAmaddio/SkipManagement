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
    public class BookingListController : AbstractFormListController<Booking>
    {
        public override AbstractClause InstantiateSearchQry() =>
        new Booking().Select().All().From();

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public override Task<IEnumerable<Booking>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(Booking model)
        {
        }
    }
}