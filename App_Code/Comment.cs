using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace SharePhotos
{
    namespace Database
    {
        namespace Entities
        {
            public class Comment
            {
                private int id;
                private string text;
                private string idUser;
                private int idPhoto;
                private bool isAcc;
                private DateTime timeAdded;

                private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationDatabase"].ConnectionString);

                private static string SqlSelect = "SELECT * FROM comments WHERE id={0}";
                private static string SqlInsert = "INSERT INTO comments (text, id_user, id_photo, time_added) VALUES ('{0}', '{1}', {2}, '{3}')";
                private static string SqlDelete = "DELETE FROM comments WHERE id={0}";
                private static string SqlUpdate = "UPDATE comments SET is_accepted=1 WHERE id={0}";

                public Comment(int id)
                {
                    SqlDataReader reader = null;
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlSelect, id.ToString()), conn);
                        reader = command.ExecuteReader();
                        if (reader.Read() == false)
                        {
                            throw new Exception("There is no comment having the id=" + id);
                        }
                        this.id = id;
                        this.idUser = reader["id_user"].ToString();
                        this.text = reader["text"].ToString();
                        this.idPhoto = int.Parse(reader["id_photo"].ToString());
                        this.isAcc = int.Parse(reader["is_accepted"].ToString()) == 1;
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
                public string getText() { return text; }
                public string getUserId() { return idUser; }
                public int getPhotoId() { return idPhoto; }
                public bool isAccepted() { return isAcc; }
                public DateTime getTime() { return timeAdded; }

                public void delete() {
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlDelete, getId().ToString()), conn);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not delete comment", ex);
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                    }
                }

                public void accept() {
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlUpdate, getId().ToString()), conn);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not update comment", ex);
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                    }
                }

                public static void createComment(string text, string id_user, int id_photo)
                {
                    try
                    {
                        DatabaseUtils.safeOpen(conn);
                        SqlCommand command = new SqlCommand(String.Format(SqlInsert, text, id_user, id_photo, DateTime.Now.ToString()), conn);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not create comment", ex);
                    }
                    finally
                    {
                        DatabaseUtils.safeClose(conn);
                    }
                }
            }
        }
    }
}