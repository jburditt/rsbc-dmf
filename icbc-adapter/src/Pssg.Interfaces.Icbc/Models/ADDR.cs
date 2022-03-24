// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Pssg.Interfaces.Icbc.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ADDR
    {
        /// <summary>
        /// Initializes a new instance of the ADDR class.
        /// </summary>
        public ADDR()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ADDR class.
        /// </summary>
        public ADDR(string sTDI = default(string), string sTTY = default(string), string sITE = default(string), string sTNO = default(string), string rURR = default(string), string pOST = default(string), string sTNM = default(string), string bUNO = default(string), string pROV = default(string), string aPR3 = default(string), string cNTY = default(string), string pOBX = default(string), string cOMP = default(string), string aPR1 = default(string), string aPR2 = default(string), System.DateTime? eFDT = default(System.DateTime?), string cITY = default(string), string pSTN = default(string))
        {
            STDI = sTDI;
            STTY = sTTY;
            SITE = sITE;
            STNO = sTNO;
            RURR = rURR;
            POST = pOST;
            STNM = sTNM;
            BUNO = bUNO;
            PROV = pROV;
            APR3 = aPR3;
            CNTY = cNTY;
            POBX = pOBX;
            COMP = cOMP;
            APR1 = aPR1;
            APR2 = aPR2;
            EFDT = eFDT;
            CITY = cITY;
            PSTN = pSTN;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "STDI")]
        public string STDI { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "STTY")]
        public string STTY { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "SITE")]
        public string SITE { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "STNO")]
        public string STNO { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "RURR")]
        public string RURR { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "POST")]
        public string POST { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "STNM")]
        public string STNM { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "BUNO")]
        public string BUNO { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PROV")]
        public string PROV { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "APR3")]
        public string APR3 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CNTY")]
        public string CNTY { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "POBX")]
        public string POBX { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "COMP")]
        public string COMP { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "APR1")]
        public string APR1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "APR2")]
        public string APR2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonConverter(typeof(DateJsonConverter))]
        [JsonProperty(PropertyName = "EFDT")]
        public System.DateTime? EFDT { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CITY")]
        public string CITY { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PSTN")]
        public string PSTN { get; set; }

    }
}