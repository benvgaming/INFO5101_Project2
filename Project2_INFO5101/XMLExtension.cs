using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Project2_INFO5101
{
    public class XMLExtension
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration xmldecl;
        XmlWriter writer = XmlWriter.Create("test.xml");
        public void WriteStartDocument()
        {
//            xmldecl = doc.CreateXmlDeclaration("1.0","UTF-8",null);

            writer.WriteStartDocument();
/*            writer.WriteStartDocument();
            writer.WriteStartElement("root");
*/        }

        public void WriteStartRootElement()
        {
            //XmlElement root = doc.DocumentElement;
/*            XmlNode node = doc.CreateNode("document","root","");
            doc.AppendChild(node);
*/            //doc.AppendChild();
            //doc.InsertBefore(xmldecl, node);



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
