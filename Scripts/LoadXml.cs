

using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class LoadXml : MonoBehaviour
{
    public string LevelNumber = "1";
    public TextAsset xmlRawFile; 

    //----------------------------
    public string[] m_coord = new string[1000]; // Check from Inspector!!!!
    public  XmlNode[] m_nodes = new XmlNode[1000];

    public int Limit = 320;  // Change from Inspector!!!!
    //----------------------------

    public string[] m_pos = new string[1000]; // Check from Inspector!!!!
    public XmlNode[] m_nodesPos = new XmlNode[1000];



    //----------------------------

    public string[] m_sound = new string[1000]; // Check from Inspector!!!!
    public XmlNode[] m_nodesSound = new XmlNode[1000];


    

    // Use this for initialization
    void Start()
    {

        //REMOVE COMMENT WHEN COMPLILE
        LevelLoader LL = (LevelLoader)FindObjectOfType(typeof(LevelLoader));
        LevelNumber = LL.Level.ToString(); 

        //
        PoolManager.instance.GlobalSpeed = PoolManager.instance.BPM[int.Parse(LevelNumber) - 1] * 0.004933333f;
        Limit = PoolManager.instance.limit[int.Parse(LevelNumber) - 1];

        PoolManager.instance.sp.counterLimit = PoolManager.instance.limit[int.Parse(LevelNumber) - 1];

        string data = xmlRawFile.text;
        parseXmlFile(data);


        parseXmlFilePos(data);

        parseXmlFileSounds(data);
    }

    void parseXmlFile(string xmlData)
    {
    
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//levels/level" + LevelNumber;
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);


        foreach (XmlNode node in myNodeList)
        {
           // XmlNode cord1 = node.FirstChild;

            m_nodes[0] = node.FirstChild;

            //init 1
            m_coord[0] = m_nodes[0].InnerXml;

            //init rest
            for (int i = 1; i <= Limit; i++)
            {
                m_nodes[i] = m_nodes[i - 1].NextSibling;

                m_coord[i] = m_nodes[i].InnerXml;

            }


         
        }
    }



    void parseXmlFilePos(string xmlData)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//levels/position"+ LevelNumber;
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);


        foreach (XmlNode node in myNodeList)
        {
            // XmlNode cord1 = node.FirstChild;

            m_nodesPos[0] = node.FirstChild;

            //init 1
            m_pos[0] = m_nodesPos[0].InnerXml;
     

            //init rest
            for (int i = 1; i <= Limit; i++)
            {
                m_nodesPos[i] = m_nodesPos[i - 1].NextSibling;

                m_pos[i] = m_nodesPos[i].InnerXml;

            }







        }

    }


    void parseXmlFileSounds(string xmlData)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//levels/sounds" + LevelNumber;
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);


        foreach (XmlNode node in myNodeList)
        {
            // XmlNode cord1 = node.FirstChild;

            m_nodesSound[0] = node.FirstChild;

            //init 1
            m_sound[0] = m_nodesSound[0].InnerXml;

            //init rest
            for (int i = 1; i <= Limit; i++)
            {
                m_nodesSound[i] = m_nodesSound[i - 1].NextSibling;

                m_sound[i] = m_nodesSound[i].InnerXml;

            }



        }
    }

}