# [Test](TableOfContents.Test.md).ClassListComments

**Inheritance:** object  

Tests lists and tables in comments.  

# Fields

## BulletList

**int**  

Previous text:  
* Header Term: Header Description  
* Term A: Description A  
* Term B: Description B  
* Term C: Description C  

**Remarks:**  
* Term A: Description A  
* Term B: Description B  
* Term C: Description C  

**Example A:**  
* Description A  
* Description B  
* Description C  

**Example B:**  
* Description A  
* Description B  
* Description C  

## BulletListInNumberItem

**int**  

Previous text:  
    
1. Header Term: Header Description  
2. Term A: Description A  
3.   
4. Term C: Description C  

## BulletListInNumberList

**int**  

Previous text:  
1. Header Term: Header Description  
2. Term A: Description A  
3. Term B: Description B  
4.   
5. Term C: Description C  

## InlineTagsInList

**int**  

Previous text:  
1. Riccasus  
2. Lorem [ClassSeeAlso](Test.ClassSeeAlso.md)  
3. Finitini [ClassSeeAlso.MethodWithParameters(int, int)](Test.ClassSeeAlso.md)  

## NumberList

**int**  

Previous text:  
1. Header Term: Header Description  
2. Term A: Description A  
3. Term B: Description B  
4. Term C: Description C  

**Remarks:**  
1. Term A: Description A  
2. Term B: Description B  
3. Term C: Description C  

**Example A:**  
1. Description A  
2. Description B  
3. Description C  

**Example B:**  
1. Description A  
2. Description B  
3. Description C  

## NumberListInBulletItem

**int**  

Previous text:  
    
* Header Term: Header Description  
* Term A: Description A  
*   
* Term C: Description C  

## NumberListInBulletList

**int**  

Previous text:  
* Header Term: Header Description  
* Term A: Description A  
* Term B: Description B  
*   
* Term C: Description C  

## NumberListWithWhitespaceInItem

**int**  

Previous text:  
    
1. Header Term: Header Description  
2. Term A: Description A  
3. Term B: Description B  
4. Term C: Description C  

## Table

**int**  

Previous text:  
  

| Header 1      | Header 2      | Header 3      |
| ------------- | ------------- | ------------- |
| Row 1, Cell 1 | Row 1, Cell 2 | Row 1, Cell 3 |
| Row 2, Cell 1 | Row 2, Cell 2 | Row 2, Cell 3 |  

**Remarks:**  
| Row 1, Cell 1 | Row 1, Cell 2 | Row 1, Cell 3 |
| ------------- | ------------- | ------------- |
| Row 2, Cell 1 | Row 2, Cell 2 | Row 2, Cell 3 |  

