# [Test](TableOfContents.Test.md).ClassCode

**Inheritance:** object  

Tests include code and xml in comments.  

# Fields

## CodeBlock

**int**  

[Summary Tag]
```
int a = 0;
int b = 1;
```  

**Misc:**  

```
string c = "c";
string d = "d";
```  

## CodeBlockWithLanguage

**int**  

[Summary Tag]
```php
<?php
$txt = "Hello world!";
$x = 5;
$y = 10.5;

echo $txt;
?>
```  

**Misc:**  

```js
function myTest() {
	document.getElementById('demo').style.fontSize='35px';
}
```  

## InlineCode

**int**  

Word word `int a = 0;` word word word.  

**Remarks:**  
Code including backtics: ```a`aa``a` ```  

**Misc:**  
Misc misc misc `public static void Main(string[] args) { }` misc misc misc.  

## InlineXml

**int**  

Word word word `<html><body></body></html>` word word word.  

**Example A:**  

```xml
<!DOCTYPE html>
<html>
<body>
</body>
</html>
```  

