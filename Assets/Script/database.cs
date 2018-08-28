using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class database : MonoBehaviour {
    IDbConnection dbConnection;
    IDbCommand dbCommand;
    String conn,cmd;
    // Use this for initialization
    void Start () {
        conn = "URI=file:" + Application.dataPath + "/Plugin/duaxedb.s3db";
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateValue("dung",3000,1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeletValue(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Readers();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            InsertValue("tuan", 5000);
        }
    }
    private void Readers()
    {
        using (dbConnection = new SqliteConnection(conn)) //đối tượng trong using sẽ được tự hủy mà ko cần chờ GC
        {
            dbConnection.Open(); //tạo kết nối tới database
            dbCommand = dbConnection.CreateCommand();
            cmd = "SELECT * " + "FROM HIGHSCORE";// HIGHSCORE là tên table
            dbCommand.CommandText = cmd;
            IDataReader reader = dbCommand.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int highScore = reader.GetInt32(2);

                Debug.Log("ID= " + id + "  name =" + name + "  HIGHSCORE =" + highScore);
            }
            reader.Close();
            reader = null;
            dbCommand.Dispose();
            dbCommand = null;
            dbConnection.Close();
            dbConnection = null;
        }
    } // đọc tất cả data có trong database
    private void DeletValue(int id) // delete cái dữ liệu có id
    {
        using (dbConnection = new SqliteConnection(conn))
        {
            dbConnection.Open();
            dbCommand = dbConnection.CreateCommand();
            cmd = string.Format("Delete from HIGHSCORE WHERE ID=\"{0}\"", id);
            dbCommand.CommandText = cmd;
            dbCommand.ExecuteScalar();
            dbConnection.Close();
        }
    }
    private void UpdateValue(string name, int highScore, int id)
    {
        using (dbConnection = new SqliteConnection(conn))
        {

            dbConnection.Open();
            dbCommand = dbConnection.CreateCommand();
            cmd = string.Format("UPDATE HIGHSCORE set name=\"{0}\", score=\"{1}\" WHERE id=\"{2}\" ", name, highScore, id);// table name
            dbCommand.CommandText = cmd;
            dbCommand.ExecuteScalar();
            dbConnection.Close();
        }
    } // update database
    private void InsertValue(string name, int score)
    {
        using (dbConnection = new SqliteConnection(conn))
        {
            dbConnection.Open();
            dbCommand = dbConnection.CreateCommand();
            cmd = string.Format("insert into HIGHSCORE (name, score) values (\"{0}\",\"{1}\")", name, score);// table name
            dbCommand.CommandText = cmd;
            dbCommand.ExecuteScalar();
            dbConnection.Close();
        }
    } // thêm 1 đối tượng trong database
}