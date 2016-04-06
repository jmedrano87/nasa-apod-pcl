using System;
using System.Runtime.Serialization;

namespace ApodPcl

{
    /// <summary>
    /// Represents an Astrononmy Picture of the Day. Each field matches one returned by NASA's API.
    /// </summary>
    [DataContract]
    public class APOD
    {

        /// <summary>
        /// Specified if media is not public domain.
        /// </summary>
        [DataMember]
        public string copyright { get; set; }
        /// <summary>
        /// The date this media was the Astronomy Picture of the day.
        /// </summary>
        [DataMember]
        public DateTime date { get; set; }

        /// <summary>
        /// A description of the media, or the <see cref="Exception.Message"/> of a <see cref="System.Net.Http.HttpRequestException"/>.
        /// </summary>
        [DataMember]
        public string explanation { get; set; }

        /// <summary>
        /// The location of the High Definition version of the image. Null when media is a video.
        /// </summary>
        [DataMember]
        public Uri hdurl { get; set; }

        /// <summary>
        /// The media's type - "image" or "video".
        /// </summary>
        [DataMember]
        public string media_type { get; set; }

        [DataMember]
        public string service_version { get; set; }

        /// <summary>
        /// The media's title, or "Error" if a <see cref="System.Net.Http.HttpRequestException"/> occured.
        /// </summary>
        [DataMember]
        public string title { get; set; }

        /// <summary>
        /// The location of the video, or if an image, the low quality version.
        /// </summary>
        [DataMember]
        public Uri url { get; set; }
    }
}
