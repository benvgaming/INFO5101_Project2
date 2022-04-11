/* Authors: Manh Cuong Nguyen, Devon Tully, James Thornton, Sabrina Umeri
 * Class: XMLExtension.cs
 * Purposes: To create XML file
 */

using System.Xml;
using System.Linq;
namespace Project2_INFO5101
{
    public static class XMLExtension
    {
        //static XmlWriter writer;

        const string PATH_XML = ".\\..\\..\\..\\..\\Data\\Summary.xml";


        public static void WriteStartDocument(this XmlWriter writer)
        {
            writer.WriteStartDocument();
        }//WriteStartDocument

        public static void WriteStartRootElement(this XmlWriter writer)
        {
            writer.WriteStartElement("root");
        }//WriteStartRootELement

        public static void WriteEndRootElement(this XmlWriter writer)
        {
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }//WriteEndRootElement

        public static void WriteStartElement(this XmlWriter writer,string element)
        {
            writer.WriteStartElement(element);
        }//WrtieStartElement

        public static void WriteEndElement(this XmlWriter writer)
        {
            writer.WriteEndElement();
        }//WriteEndElement

        public static void WriteAttribute(this XmlWriter writer,string attribute)
        {
            writer.WriteString(attribute);
        }//WriteAttribute
    }//End of class
}//End of namespace
