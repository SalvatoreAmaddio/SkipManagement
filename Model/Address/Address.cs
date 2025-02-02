﻿using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace SkipManagement.Model
{
    [Table(nameof(Address))]
    public class Address : AbstractModel<Address>
    {
        #region backing fields
        private long _addressid;
        private string _streetNum = string.Empty;
        private string _streetName = string.Empty;
        private string _furtherInfo = string.Empty;
        private PostCode? _postCode;
        private City? _city;
        #endregion

        #region Properties
        [PK]
        public long AddressID { get => _addressid; set => UpdateProperty(ref value, ref _addressid); }
        [Field]
        public string StreetNum { get => _streetNum; set => UpdateProperty(ref value, ref _streetNum); }
        [Field]
        public string StreetName { get => _streetName; set => UpdateProperty(ref value, ref _streetName); }
        [Field]
        public string FurtherInfo { get => _furtherInfo; set => UpdateProperty(ref value, ref _furtherInfo); }
        [FK]
        public PostCode? PostCode { get => _postCode; set => UpdateProperty(ref value, ref _postCode); }
        public City? City => PostCode?.City;
        #endregion

        #region Constructors
        public Address()
        {
            SelectQry = this.Select().All().Fields("PostCode.Code").Fields("City.CityID").Fields("CityName").From().InnerJoin(nameof(PostCode), "PostCodeID").InnerJoin(nameof(PostCode), nameof(City), "CityID").Statement();
            AfterUpdate += OnAfterUpdate;
        }

        public Address(long id) : this() => _addressid = id;
        public Address(long id, string num, string name, string info) : this(id) 
        {
            _streetNum = num;
            _streetName = name;
            _furtherInfo = info;
        }

        public Address(long id, string num, string name, string info, PostCode code) : this(id,num,name,info)
        {
            _postCode = code;
        }

        public Address(DbDataReader db) : this()
        {
            _addressid = db.GetInt64(0);
            _streetNum = db.GetString(1);
            _streetName = db.GetString(2);
            _furtherInfo= db.GetString(3);
            _postCode = new(db.GetInt64(4), db.GetString(5), new(db.GetInt64(6), db.GetString(7)));
        }
        #endregion

        private void OnAfterUpdate(object? sender, FrontEnd.Events.AfterUpdateArgs e)
        {
            if (e.Is(nameof(StreetName))) 
                _streetName = StreetName.FirstLetterCapital();
            if (e.Is(nameof(FurtherInfo)))
                _furtherInfo = FurtherInfo.FirstLetterCapital();
            if (e.Is(nameof(PostCode)))
                RaisePropertyChanged(nameof(City));
        }

        public override string ToString() => $"{StreetNum}, {StreetName} {FurtherInfo}. {PostCode}, {City}";
    }
}
