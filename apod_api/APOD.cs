using System.Runtime.Serialization;

namespace ApodPcl

{
    [DataContract]
    public class APOD
    {
        [DataMember]
        public string copyright { get; set; }

        [DataMember]
        public System.DateTime date { get; set; }

        [DataMember]
        public string explanation { get; set; }

        [DataMember]
        public string hdurl { get; set; }

        [DataMember]
        public string media_type { get; set; }

        [DataMember]
        public string service_version { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string url { get; set; }
    }
}
