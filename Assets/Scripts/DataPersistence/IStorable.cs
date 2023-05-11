using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorable
{
    string Id { get; } // id of an object
    
    void Load(string data); // load default data witch is string var

    string Save(); //default save string 
}

public interface IStorable<T> : IStorable {
    void IStorable.Load(string data) => LoadData(string.IsNullOrEmpty(data) ? default : JsonUtility.FromJson<T>(data));
    
    string IStorable.Save() => JsonUtility.ToJson(SaveData());

    void LoadData(T data);
    
    T SaveData();
}