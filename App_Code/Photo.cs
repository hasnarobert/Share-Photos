using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using SharePhotos.Database;

namespace SharePhotos
{
    namespace Database
    {
        namespace Entities
        {
            public class Photo
            {
                private int id;
                private int idAlbum;
                private int idCategory;
                private string description;
                private DateTime timeAdded;

                private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationDatabase"].ConnectionString);

                private static string SqlSelect = "SELECT * FROM photos WHERE id={0}";
                private static string SqlSelectComments = "SELECT id FROM comments WHERE id_photo={0} ORDER BY time_added DESC";
                private static string SqlInsert = "INSERT INTO photos (id_album, id_category, description, time_added) VALUES ({0}, {1}, '{2}', '{3}')";
                private static string SqlSelectAll = "SELECT * FROM photos ORDER BY time_added DESC";
                private static string SqlSelectSearch = "SELECT * FROM photos WHERE description LIKE '%{0}%' ORDER BY time_added DESC";
                private static string SqlDelete = "DELETE FROM photos WHERE id={0}";

                public Photo(int id, int idAlbum, int idCategory, string description, DateTime timeAdded) {
                    this.id = id;
                    this.idAlbum = idAlbum;
                    this.idCategory = idCategory;
                    this.description = description;
                    this.timeAdded = timeAdded;
                }

                public Photo(int id)
                {
                    SqlDataReader reader = null;
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlSelect, id.ToString()), conn);
                        reader = command.ExecuteReader();
                        if (reader.Read() == false)
                        {
                            throw new Exception("There is no photo having the id=" + id);
                        }
                        this.id = id;
                        this.idAlbum = int.Parse(reader["id_album"].ToString());
                        this.idCategory = int.Parse(reader["id_category"].ToString());
                        this.description = reader["description"].ToString();
                        this.timeAdded = DateTime.Parse(reader["time_added"].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                        DatabaseUtils.safeClose(reader);
                    }
                }

                public int getId() { return id; }
                public int getAlbumId() { return idAlbum; }
                public int getCategoryId() { return idCategory; }
                public string getDescription() { return description; }

                public Category getCategory()
                {
                    return new Category(getCategoryId());
                }

                public Album getAlbum()
                {
                    return new Album(getAlbumId());
                }

                public Comment[] getComments()
                {
                    SqlDataReader reader = null;
                    ArrayList temp = new ArrayList();
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlSelectComments, getId().ToString()), conn);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            temp.Add(new Comment(int.Parse(reader["id"].ToString())));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                        DatabaseUtils.safeClose(reader);
                    }

                    return (Comment[])temp.ToArray(typeof(Comment));
                }

                public void delete() {
                    Comment[] comments = getComments();
                    for (int i = 0; i < comments.Length; ++i) {
                        comments[i].delete();
                    }

                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlDelete, getId().ToString()), conn);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not delete photo", ex);
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                    }
                }

                public static int createPhoto(int id_album, int id_category, string description)
                {
                    int idReturned = -1;
                    SqlDataReader reader = null;
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlInsert, id_album, id_category, description, DateTime.Now.ToString()), conn);
                        command.ExecuteNonQuery();

                        SqlCommand command2 = new SqlCommand("SELECT MAX(id) AS maxim FROM photos;", conn);
                        reader = command2.ExecuteReader();
                        if (reader.Read() == false)
                        {
                            throw new Exception("A crapat infect");
                        }
                        idReturned = int.Parse(reader["maxim"].ToString());

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not create photo ", ex);
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                    }


                    return idReturned;
                }

                public static Photo[] getAllPhotos(int category = -1)
                {
                    SqlDataReader reader = null;
                    ArrayList temp = new ArrayList();
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(SqlSelectAll, conn);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = int.Parse(reader["id"].ToString());
                            int idAlbum = int.Parse(reader["id_album"].ToString());
                            int idCategory = int.Parse(reader["id_category"].ToString());
                            string description = reader["description"].ToString();
                            DateTime timeAdded = DateTime.Parse(reader["time_added"].ToString());
                            if (idCategory == category || category == -1)
                                temp.Add(new Photo(id, idAlbum, idCategory, description, timeAdded));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                        DatabaseUtils.safeClose(reader);
                    }

                    return (Photo[])temp.ToArray(typeof(Photo));
                }

                public static Photo[] search(string query)
                {
                    SqlDataReader reader = null;
                    ArrayList temp = new ArrayList();
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlSelectSearch, query), conn);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = int.Parse(reader["id"].ToString());
                            int idAlbum = int.Parse(reader["id_album"].ToString());
                            int idCategory = int.Parse(reader["id_category"].ToString());
                            string description = reader["description"].ToString();
                            DateTime timeAdded = DateTime.Parse(reader["time_added"].ToString());
                            temp.Add(new Photo(id, idAlbum, idCategory, description, timeAdded));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                        DatabaseUtils.safeClose(reader);
                    }

                    return (Photo[])temp.ToArray(typeof(Photo));
                }
            }
        }
    }
}