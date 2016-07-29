using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.DataModel
{
    public abstract class CModelBase
    {
        public int Id { get; set; }


        // NOTE: The Timestamp attribute specifies that this column will be included in the Where clause of Update and Delete.
        [Timestamp]
        public byte[] RowVersion { get; set; }

        private static Dictionary<string, string> _LanguageCodes;
        public static IDictionary<string, string> LanguageCodes
        {
            get
            {
                if (_LanguageCodes == null)
                {
                    _LanguageCodes = new Dictionary<string, string>();
                    foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(c => c.Parent).OrderBy(c => c.DisplayName))
                    {
                        if (!_LanguageCodes.ContainsKey(cultureInfo.ThreeLetterISOLanguageName))
                            _LanguageCodes.Add(cultureInfo.ThreeLetterISOLanguageName, cultureInfo.DisplayName);
                    }
                }

                return _LanguageCodes;
            }
        }

        private static Dictionary<string, string> _CountryCodes;
        public static IDictionary<string, string> CountryCodes
        {
            get
            {
                if (_CountryCodes == null)
                {
                    var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(c => new RegionInfo(c.LCID)).Distinct().OrderBy(r => r.DisplayName);
                    _CountryCodes = regions.ToDictionary(r => r.ThreeLetterISORegionName, r => r.DisplayName);
                }
                return _CountryCodes;

            }
        }


    }
}