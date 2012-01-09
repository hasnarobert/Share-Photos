using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace SharePhotos
{
    namespace Database
    {
        namespace Entities
        {
            public class Album
            {
                private int id;
                private string idUser;
                private string name;

                private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationDatabase"].ConnectionString);

                private static string SqlSelect = "SELECT * FROM albums WHERE id={0}";
                private static string SqlSelectPhotos = "SELECT id FROM photos WHERE id_album={0}";
                private static string SqlInsert = "INSERT INTO albums (name, id_user) VALUES ('{0}', '{1}')";
                private static string SqlSelectAll = "SELECT * FROM albums WHERE id_user='{0}'";


                public Album(int id, string idUser, string name) 
                {
                    this.id = id;
                    this.idUser = idUser;
                    this.name = name;
                }

                public Album(int id)
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(String.Format(SqlSelect, id.ToString()), conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read() == false)
                        {
                            throw new Exception("There is no album having the id=" + id);
                        }
                        this.id = id;
                        this.idUser = reader["id_user"].ToString();
                        this.name = reader["name"].ToString();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                public int getId() { return id; }
                public string getUserId() { return idUser; }
                public string getName() { return name; }

                public Photo[] getPhotos()
                {
                    ArrayList temp = new ArrayList();
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(String.Format(SqlSelectPhotos, getId().ToString()), conn);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            temp.Add(new Photo(int.Parse(reader["id"].ToString())));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }

                    Photo[] result = new Photo[temp.Count];
                    return (Photo[])temp.ToArray(typeof(Photo));
                }

                public static void createAlbum(string id_user, string name)
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(String.Format(SqlInsert, name, id_user), conn);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not create album", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                public static Album[] getAllAlbums(string userName)
                {
                    ArrayList temp = new ArrayList();
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(String.Format(SqlSelectAll, userName), conn);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            temp.Add(new Album(int.Parse(reader["id"].ToString()), reader["id_user"].ToString(), reader["name"].ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return (Album[])temp.ToArray(typeof(Album));
                }
            }
        }
    }
}