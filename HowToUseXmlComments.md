# How to Use XML Comments in .Net

## summary

The summary is the text shown by Visual Studio Intellisense.  

Only one summary tag is expected.

```xml
/// <summary>
/// Description of the purpose of this type or member.
/// </summary>
```

## remarks

Remarks provide more explanation than the summary.

```xml
/// <remarks>
/// More comprehensive description of the type or member.
/// Anything important you want to say that doesn't fit in a more specific tag.
/// </remarks>
```

## param

Describe the purpose or usage of a method's parameter.  

Only expected on methods.  
Only one param tag is expected for each parameter.

```xml
/// <param name="x">Description of parameter "x".</param>
/// <param name="y">Description of parameter "y".</param>
public void Method(int x, string y)
{
}
```

## typeparam

Describe the purpose or usage of a class's or a method's type parameter.

Only expected on generic types or generic methods.  
Only one typeparam tag is expected for each type parameter.

```xml
/// <typeparam name="T">Description of type parameter "T".</typeparam>
/// <typeparam name="U">Description of type parameter "U".</typeparam>
public class Class<T,U>
{
}
```

```xml
/// <typeparam name="A">Description of type parameter "A".</typeparam>
/// <typeparam name="B">Description of type parameter "B".</typeparam>
public void Method<A,B>(A a, B b, int c)
{
}
```

## returns

Describe what is returned from a method.  

Only expected on methods with a return type.  
Only one returns tag is expected.

```xml
/// <returns>Description of returned data.</returns>
public int Method()
{
	return 0;
}
```

## value

Describe a property value. [Microsoft recommends](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/value) using the value tag instead of the default summary tag for property descriptions.  

Only expected on properties.  
Only one value tag is expected.

```xml
/// <value>Description of property.</value>
public int Property { get; set; }
```

## exception

Describe all possible exceptions a method may throw. Exceptions are shown by Visual Studio Intellisense.

Only expected on methods.  
Add one exception tag for each Type of exception, or one exception tag for each cause of an exception.

```xml
/// <exception cref="ArgumentException"><paramref name="list"/> cannot be null.</exception>
/// <exception cref="FileNotFoundException">File not found.</exception>
public void Method(List<int> list, string fileName)
{
}
```

See the [cref](#cref-Attribute) section for everything you can link to.

## permission

Describe access levels for a field or member. The permission tag can be linked (cref) to any field or member in scope.

```xml
/// <permission cref="Method(int,string)">Description of access levels.</permission>
public void Method(int x, string y)
{
}
```

See the [cref](#cref) section for everything you can link to.

## example

Provide usage examples for a type or method.

```xml
/// <example>
///   Method chaining:
///   <code>
///     Button button = new Button();
///     LayoutHelper.LeftOf(controlA).Below(controlB).Apply(button);
///   </code>
/// </example>
```

## Nested Tags

These tags are only expected nested within other tags.

### code

Include formatted code, usually within an example tag.

```xml
/// <code>
/// int x = 5;
/// int y = 10;
/// int z = x * y;
/// </code>
```

### list

Include a formatted list. Lists can use bulleted or numbered.  
Lists may be nested within other lists.

List type attribute can be set to "bullet" or "number". Do not manually include the numbers in a "number" list.

```xml
/// <list type="bullet">
///   <listheader>
///     <term>Term</term>
///     <description>Description</description>
///   </listheader>
///   <item>
///     <term>Term</term>
///     <description>Description</description>
///   </item>
///   <item>
///     <term>Term</term>
///     <description>Description</description>
///   </item>
/// </list>
```

```xml
/// <list type="bullet">
///   <item>
///     <description>Description</description>
///   </item>
///   <item>
///     <description>Description</description>
///   </item>
/// </list>
```

### table

Include a formatted table.

```xml
/// <list type="table">
///   <listheader>
///     <term>Header row, Cell 1</term>
///     <term>Header row, Cell 2</term>
///     <term>Header row, Cell 3</term>
///   </listheader>
///   <item>
///     <term>Row 2, Cell 1</term>
///     <term>Row 2, Cell 2</term>
///     <term>Row 2, Cell 3</term>
///   </item>
///   <item>
///     <term>Row 3, Cell 1</term>
///     <term>Row 3, Cell 2</term>
///     <term>Row 3, Cell 3</term>
///   </item>
/// </list>
```

## Inline Tags

These tags are only expected within normal text.

### para

Separate text into paragraphs.

```xml
/// <remarks>
///   <para>First paragraph.</para>
///   <para>Second paragraph.</para>
///   <para>Third paragraph.</para>
/// </remarks>
```

### c

Mark a section of in-line code.

```xml
/// <example>
/// The modulus operation <c>x % m</c> results in a number between 0 and m.
/// </example>
```

### see

Make a reference to another type, field, or member.

```xml
/// <remarks>...similar to the <see cref="OtherMethod()"/> method.</remarks>
```

See the [cref](#cref-Attribute) section for everything you can link to.

### seealso

Make a reference to another type, field, or member. This is a less important reference than a see tag.

```xml
/// <remarks>...similar to the <seealso cref="OtherMethod()"/> method.</remarks>
```

See the [cref](#cref-Attribute) section for everything you can link to.

### CDATA

Include formatted xml within the xml comments by enclosing them in a CDATA tag.

```xml
/// <example>
///   <![CDATA[
///     <html>
///       <body>
///       </body>
///     </html>
///   ]]>
/// </example>
```

### paramref

Make a reference to a parameter in the current method.

```xml
/// <remarks>...something about the <paramref name="x"/> parameter...</remarks>
public void Method(int x)
{
}
```

### typeparamref

Make a reference to a type parameter in the current type or method.

```xml
/// <remarks>...something about the <typeparamref name="T"/> type parameter...</remarks>
public class Class<T>
{
}
```

```xml
/// <remarks>...something about the <typeparamref name="A"/> type parameter...</remarks>
public void Method<A>(A a, int x)
{
}
```

## cref Attribute

How to link to almost anything in your code.

**Normal Type**

```xml
/// <see cref="MyType"/>
/// <see cref="FullNamespace.MyType"/>
public class MyType
{
}
```

**Generic Type**

```xml
/// <see cref="MyType{T,U}"/>
/// <see cref="FullNamespace.MyType{T,U}"/>
public class MyType<T,U>
{
}
```

**Normal Method, Static Method**

```xml
/// <see cref="MyMethod()"/>
/// <see cref="MyType.MyMethod()"/>
/// <see cref="FullNamespace.MyType.MyMethod()"/>
public int MyMethod()
{
}
```

```xml
/// <see cref="MyMethod(int,string)"/>
/// <see cref="MyType.MyMethod(int,string)"/>
/// <see cref="FullNamespace.MyType.MyMethod(int,string)"/>
public int MyMethod(int x, string y)
{
}
```

**Generic Method**

```xml
/// <see cref="MyMethod{A,B}(A,B)"/>
/// <see cref="MyType.MyMethod{A,B}(A,B)"/>
/// <see cref="FullNamespace.MyType.MyMethod{A,B}(A,B)"/>
public int MyMethod<A,B>(A a, B b)
{
}
```

**Constructor Method**

```xml
/// <see cref="MyType()"/>
/// <see cref="MyType.MyType()"/>
/// <see cref="FullNamespace.MyType.MyType()"/>
public MyType()
{
}
```

```xml
/// <see cref="MyType(int,string)"/>
/// <see cref="MyType.MyType(int,string)"/>
/// <see cref="FullNamespace.MyType.MyType(int,string)"/>
public MyType(int x, string y)
{
}
```

**Static Constructor Method**

If you know how to cref a static constructor, please message me.

**Destructor or Finalize Method**

```xml
/// <see cref="Finalize()"/>
/// <see cref="MyType.Finalize()"/>
/// <see cref="FullNamespace.MyType.Finalize()"/>
~MyType()
{
}
```

**Field**

```xml
/// <see cref="MyField"/>
/// <see cref="MyType.MyField"/>
/// <see cref="FullNamespace.MyType.MyField"/>
public int MyField = 0;
```

**Property**

```xml
/// <see cref="MyProperty"/>
/// <see cref="MyType.MyProperty"/>
/// <see cref="FullNamespace.MyType.MyProperty"/>
public int MyProperty { get; set; }
```

**Indexer**

```xml
/// <see cref="this[int]"/>
/// <see cref="MyType.this[int]"/>
/// <see cref="FullNamespace.MyType.this[int]"/>
public int this[int key] { get { return 0; } set; }
```

**Implicit Operator**

If there is more than one implicit operator, be specific:
```xml
/// <see cref="implicit operator int(MyType)"/>
/// <see cref="MyType.implicit operator int(MyType)"/>
/// <see cref="FullNamespace.MyType.implicit operator int(MyType)"/>
public static implicit operator int(MyType x) { }
```

If there is only one implicit operator, you can use this:
```xml
/// <see cref="op_Implicit(MyType)"/>
/// <see cref="MyType.op_Implicit(MyType)"/>
/// <see cref="FullNamespace.MyType.op_Implicit(MyType)"/>
public static implicit operator int(MyType x) { }
```

**Explicit Operator**

If there is more than one explicit operator, be specific:
```xml
/// <see cref="explicit operator int(MyType)"/>
/// <see cref="MyType.explicit operator int(MyType)"/>
/// <see cref="FullNamespace.MyType.explicit operator int(MyType)"/>
public static explicit operator int(MyType x) { }
```

If there is only one explicit operator, you can use this:
```xml
/// <see cref="op_Explicit(MyType)"/>
/// <see cref="MyType.op_Explicit(MyType)"/>
/// <see cref="FullNamespace.MyType.op_Explicit(MyType)"/>
public static explicit operator int(MyType x) { }
```

**Overloaded Operators**

There are two valid formats for most operators. Some operators use xml control characters, so only their long form is valid within xml comments.

```xml
/// <see cref="operator +(MyType,int)"/>
/// <see cref="op_Addition(MyType,int)"/>
public static MyType operator +(MyType x, int y) { }
```

```xml
/// <see cref="operator -(MyType,int)"/>
/// <see cref="op_Subtraction(MyType,int)"/>
public static MyType operator -(MyType x, int y) { }
```

```xml
/// <see cref="operator *(MyType,int)"/>
/// <see cref="op_Multiply(MyType,int)"/>
public static MyType operator *(MyType x, int y) { }
```

```xml
/// <see cref="operator /(MyType,int)"/>
/// <see cref="op_Division(MyType,int)"/>
public static MyType operator /(MyType x, int y) { }
```

```xml
/// <see cref="operator %(MyType,int)"/>
/// <see cref="op_Modulus(MyType,int)"/>
public static MyType operator %(MyType x, int y) { }
```

```xml
/// <see cref="op_BitwiseAnd(MyType,int)"/>
public static MyType operator &(MyType x, int y) { }
```

```xml
/// <see cref="operator |(MyType,int)"/>
/// <see cref="op_BitwiseOr(MyType,int)"/>
public static MyType operator |(MyType x, int y) { }
```

```xml
/// <see cref="operator true(MyType)"/>
/// <see cref="op_True(MyType)"/>
public static bool operator true(MyType x) { }
```

```xml
/// <see cref="operator false(MyType)"/>
/// <see cref="op_False(MyType)"/>
public static bool operator false(MyType x) { }
```

```xml
/// <see cref="operator ^(MyType,int)"/>
/// <see cref="op_ExclusiveOr(MyType,int)"/>
public static MyType operator ^(MyType x, int y) { }
```

```xml
/// <see cref="operator ~(MyType)"/>
/// <see cref="op_OnesComplement(MyType)"/>
public static MyType operator ~(MyType x) { }
```

```xml
/// <see cref="operator !(MyType)"/>
/// <see cref="op_LogicalNot(MyType)"/>
public static MyType operator !(MyType x) { }
```

```xml
/// <see cref="operator ==(MyType,int)"/>
/// <see cref="op_Equality(MyType,int)"/>
public static bool operator ==(MyType x, int y) { }
```

```xml
/// <see cref="operator !=(MyType,int)"/>
/// <see cref="op_Inequality(MyType,int)"/>
public static bool operator !=(MyType x, int y) { }
```

```xml
/// <see cref="operator >(MyType,int)"/>
/// <see cref="op_GreaterThan(MyType,int)"/>
public static bool operator >(MyType x, int y) { }
```

```xml
/// <see cref="op_LessThan(MyType,int)"/>
public static bool operator <(MyType x, int y) { }
```

```xml
/// <see cref="operator >=(MyType,int)"/>
/// <see cref="op_GreaterThanOrEqual(MyType,int)"/>
public static bool operator >=(MyType x, int y) { }
```

```xml
/// <see cref="op_LessThanOrEqual(MyType,int)"/>
public static bool operator <=(MyType x, int y) { }
```

```xml
/// <see cref="operator --(MyType)"/>
/// <see cref="op_Decrement(MyType)"/>
public static MyType operator --(MyType x) { }
```

```xml
/// <see cref="operator ++(MyType)"/>
/// <see cref="op_Increment(MyType)"/>
public static MyType operator ++(MyType x) { }
```

```xml
/// <see cref="operator >>(MyType,int)"/>
/// <see cref="op_RightShift(MyType,int)"/>
public static MyType operator >>(MyType x, int y) { }
```

```xml
/// <see cref="op_LeftShift(MyType,int)"/>
public static MyType operator <<(MyType x, int y) { }
```

