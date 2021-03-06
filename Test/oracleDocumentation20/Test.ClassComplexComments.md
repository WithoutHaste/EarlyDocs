# [Test](TableOfContents.Test.md).ClassComplexComments

**Inheritance:** object  

[Summary Tag] [Multiline] Ut eleifend dignissim viverra. Donec ut nisl id augue dapibus   
congue eget et risus. Nulla id gravida felis, nec viverra lectus.   
  
Nullam euismod pretium laoreet.Suspendisse tempus ac est quis hendrerit. Nulla vitae   
sem egestas, consequat urna sed, malesuada erat. Integer tempor at neque in volutpat. Nulla   
placerat lacus sit amet massa lobortis, ut eleifend libero venenatis. Vivamus sit amet   
leo diam. Nullam pulvinar iaculis nunc, eu tristique elit.   
  
Interdum et malesuada fames ac ante ipsum primis in faucibus.Praesent dapibus euismod nibh,   
ac sollicitudin mi iaculis non.  

**Remarks:**  
[Remarks Tag] [Multiline] Nulla vulputate eros sit amet libero volutpat   
dignissim. Etiam rutrum imperdiet felis, id hendrerit magna sollicitudin   
nec.   
  
Donec sapien risus, feugiat elementum luctus at, sagittis et arcu.   
Aliquam arcu arcu, porta quis libero id, sodales ornare elit. Maecenas risus   
nulla, consequat eget fermentum eu, ultrices at neque. Aenean quis ligula   
vel dolor auctor rhoncus. Aliquam aliquam urna vitae varius vestibulum. Mauris   
condimentum risus a mi condimentum viverra.  
    
1. Mollis  
2. Ante  
3. Ornare  
4. Ipsum  
5. Faucibus  
6. Luctus  
7. Posuere  

# Examples

## Example A:

[Example Tag] [Multiline] Donec lectus odio, mollis id ante ut, ornare pretium lacus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla nec justo varius sem consequat bibendum vitae eget nisl.  
  
```
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Read in lines from file.
        foreach(string line in File.ReadLines("c:\\file.txt"))
        {
            Console.WriteLine("-- {0}", line);
        }
    }
}
```

Proin sollicitudin non est eget semper. Suspendisse imperdiet turpis ac elit hendrerit dapibus ut a lacus. Curabitur pulvinar lacus at risus condimentum, eget ornare ligula tempus.  

## Example B:

[Example Tag] [Multiline] Aliquam sed mi convallis, convallis purus congue, volutpat augue.  
  
Donec lobortis consequat ligula in faucibus. Ut vitae sapien vel lacus malesuada pharetra id id nisl. Vestibulum vel felis enim.  
  
  
```
string[] lines = File.ReadAllLines("C:\\rearrange.txt");
          
Console.WriteLine("Length: {0}", lines.Length);
Console.WriteLine("First: {0}", lines[0]);
          
int count = 0;
foreach (string line in lines)
{
   count++;
}
          
int c = 0;
for (int i = 0; i < lines.Length; i++)
{
    c++;
}
```  

# Methods

## MethodComplexExceptions&lt;A&gt;(string x, A y, int z)

**void**  

### Exceptions

#### [System.IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.indexoutofrangeexception):

[1st Exception Tag] [Has Example] Cras ante risus, sodales quis massa vel, pulvinar auctor ipsum.  

**Example A:**  
Fusce ligula ipsum, porttitor at velit ut, commodo auctor ligula. Nunc imperdiet, justo sed bibendum laoreet, nibh purus laoreet dolor,   
vitae consectetur sem erat quis ipsum. Nam tincidunt non magna in mattis. Curabitur nec efficitur sapien, vel rutrum odio.  

#### [System.IO.FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception):

[2nd Exception Tag] [Has ParamRef] Nulla facilisi. Donec fermentum nisl felis,  

_x_  

 ac  

_y_  

 venenatis tellus tincidunt ultrices.  

#### [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception):

[3rd Exception Tag] [Has TypeParamRef] Vestibulum ante ipsum primis in faucibus orci  

_A_  

 luctus et ultrices posuere cubilia Curae; Suspendisse id auctor libero.  

## MethodComplexParamsAndTypeParams&lt;A&gt;(string x, A y, int z)

**void**  

### Generic Type Parameters

#### A

[TypeParam Tag] [Has Example]  

Nunc mollis leo est, in bibendum lacus rhoncus maximus.  

**Example A:**  
Donec euismod mollis dui, non bibendum quam porta non.  

### Parameters

#### int z

[Param Rag] [Has Example]  

Vestibulum convallis sapien eget nunc sagittis cursus. Pellentesque suscipit urna nulla,   
non vestibulum enim tempus euismod. Suspendisse mattis purus at mauris imperdiet, vitae   
volutpat urna commodo. Cras justo dolor, aliquet a nibh sed, porttitor fringilla odio.   
  
Vestibulum metus lorem, pulvinar sed maximus ac, efficitur ac odio.  

**Example A:**  
Maecenas molestie, nisi nec cursus malesuada, nisi est egestas lacus, mollis aliquam urna elit eu mauris. Maecenas lobortis congue nisi. Praesent tincidunt sagittis mi non semper.  

