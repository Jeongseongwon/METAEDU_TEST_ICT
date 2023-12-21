using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

//Manager_data
public class DialogueData
{
    //[XmlAttribute]
    public string ID;
    public string Name;
    public string Birth_date;
    public string Date;
    public string Session;
    public string Data_1;
    public string Data_2;
}

// Manager_login
public class LoginData
{
    //[XmlAttribute]
    public string ID;
    public string Name;
    public string Birth_date;
}

// Manager_survey
public class SurveyData
{
    public string ID;
    public string Name;
    public string Date;
    public string Session;
    public string Data_S1;
    public string Data_S2;
    public string Data_S3;
    public string Data_S4;
    public string Data_S5;
    public string Data_S6;
    public string Data_S7;
    public string Data_S8;
}

public class Result_IndetailData
{
    public string ID;
    public string Name;
    public string Date;
    public string Session;
    public List<string> Data = new List<string>();
}


