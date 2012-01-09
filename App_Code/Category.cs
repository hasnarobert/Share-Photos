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
            public class Category
            {
                private int id;
                private string name;

                private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationDatabase"].ConnectionString);

                private static string SqlSelectName = "SELECT name FROM categories WHERE id={0}";
                private static string SqlSelectId = "SELECT id FROM categories WHERE name='{0}'";
                private static string SqlSelectAll = "SELECT * FROM categories";
                private static string SqlSelectPhotos = "SELECT id FROM photos WHERE id_category={0}";
                private static string SqlInsert = "INSERT INTO categories (name) VALUES ('{0}')";


                public Category(int id)
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(String.Format(SqlSelectName, id.ToString()), conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read() == false)
                        {
                            throw new Exception("There is no category having the id=" + id);
                        }
                        this.id = id;
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

                public Category(string name)
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(String.Format(SqlSelectId, name.ToString()), conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read() == false)
                        {
                            throw new Exception("There is no ctegory having the name=" + name);
                        }
                        this.id = int.Parse(reader["id"].ToString());
                        this.name = name;
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

                public Category(int id, string name)
                {
                    this.id = id;
                    this.name = name;
                }

                public int getId()
                {
                    return id;
                }

                public string getName()
                {
                    return name;
                }

                public Photo[] getPhotos()
                {
                    ArrayList temp = new ArrayList();
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(String.Format(SqlSelectPhotos, getId()), conn);
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

                public static void createCategory(string name)
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(String.Format(SqlInsert, name), conn);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not create category named " + name + ". Maybe it already exists", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                public static Category[] getAllCategories()
                {
                    ArrayList temp = new ArrayList();
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(SqlSelectAll, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            temp.Add(new Category(int.Parse(reader["id"].ToString()), reader["name"].ToString()));
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

                    return (Category[])temp.ToArray(typeof(Category));
                }

            }
        }
    }
}

            