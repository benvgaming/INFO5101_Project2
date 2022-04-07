using System.Xml;

namespace Project2_INFO5101
{
    public class XMLExtension
    {
        XmlWriter writer;
        public XMLExtension()
        {

           XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
           {
               Indent = true,
               IndentChars = "\t",
               NewLineOnAttributes = true
           };
           writer = XmlWriter.Create("test.xml", xmlWriterSettings);
        }

        public void WriteStartDocument()
        {
            writer.WriteStartDocument();
        }

        public void WriteStartRootElement()
        {
            writer.WriteStartElement("root");
        }

        public void WriteEndRootElement()
        {
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public void WriteStartElement(string element)
        {
            writer.WriteStartElement(element);
        }

        public void WriteEndElement()
        {
            writer.WriteEndElement();
        }

        public void WriteAttribute(string attribute)
        {
            writer.WriteString(attribute);
        }
    }
}
