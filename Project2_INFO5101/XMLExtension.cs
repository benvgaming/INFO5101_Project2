/* Authors: Manh Cuong Nguyen, Devon Tully, James Thornton, Sabrina Umeri
 * Class: XMLExtension.cs
 * Purposes: To create XML file
 */

using System.Xml;

namespace Project2_INFO5101
{
    public class XMLExtension
    {
        XmlWriter writer;

        const string PATH_XML = ".\\..\\..\\..\\..\\Data\\Summary.xml";
        public XMLExtension()
        {

           XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
           {
               Indent = true,
               IndentChars = "\t",
               NewLineOnAttributes = true
           };
           writer = XmlWriter.Create(PATH_XML, xmlWriterSettings);
        }//c'tor

        public void WriteStartDocument()
        {
            writer.WriteStartDocument();
        }//WriteStartDocument

        public void WriteStartRootElement()
        {
            writer.WriteStartElement("root");
        }//WriteStartRootELement

        public void WriteEndRootElement()
        {
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }//WriteEndRootElement

        public void WriteStartElement(string element)
        {
            writer.WriteStartElement(element);
        }//WrtieStartElement

        public void WriteEndElement()
        {
            writer.WriteEndElement();
        }//WriteEndElement

        public void WriteAttribute(string attribute)
        {
            writer.WriteString(attribute);
        }//WriteAttribute
    }//End of class
}//End of namespace
