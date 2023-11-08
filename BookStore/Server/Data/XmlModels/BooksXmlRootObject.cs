namespace BookStore.Server.Data.XmlModels;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class BooksXmlRootObject
{

    private BookXML[] bookField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("book")]
    public BookXML[] book
    {
        get
        {
            return this.bookField;
        }
        set
        {
            this.bookField = value;
        }
    }
}