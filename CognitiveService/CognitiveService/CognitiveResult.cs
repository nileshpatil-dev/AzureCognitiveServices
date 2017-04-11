using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService
{

    public class ImageDetails
    {
        public Category[] categories { get; set; }
        public string requestId { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public int width { get; set; }
        public int height { get; set; }
        public string format { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public float score { get; set; }
        public Detail detail { get; set; }
    }

    public class Detail
    {
        public Celebrity[] celebrities { get; set; }
    }

    public class Celebrity
    {
        public string name { get; set; }
        public Facerectangle faceRectangle { get; set; }
        public float confidence { get; set; }
    }

    public class Facerectangle
    {
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

}
