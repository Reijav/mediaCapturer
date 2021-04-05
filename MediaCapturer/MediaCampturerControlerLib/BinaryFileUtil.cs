using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MediaCampturerControlerLib
{
    public class BinaryFileUtil<T>
    {

        public T Deserialize(String filename)
        {
            T emps = (T)Activator.CreateInstance(typeof(T), new object[] { }); ;
            FileStream fs = null;
            try
            {
                //Format the object as Binary  
                BinaryFormatter formatter = new BinaryFormatter();

                //Reading the file from the server  
                fs = File.Open(filename, FileMode.Open);

                var obj = formatter.Deserialize(fs);
                emps = (T)obj;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }


            }


            return emps;
        }

        public void Serialize(T emps, String filename)
        {

            //Create the stream to add object into it.  
            System.IO.Stream ms = null;
            try
            {
                ms = File.OpenWrite(filename);
                //Format the object as Binary  

                BinaryFormatter formatter = new BinaryFormatter();
                //It serialize the employee object  
                formatter.Serialize(ms, emps);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (ms != null)
                {
                    ms.Flush();
                    ms.Close();
                    ms.Dispose();
                }
 
            }

            

        }
    }
}
