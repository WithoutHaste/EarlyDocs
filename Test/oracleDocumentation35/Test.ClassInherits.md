# [Test](TableOfContents.Test.md).ClassInherits

**Inheritance:** object → [ClassSimpleComments](Test.ClassSimpleComments.md)  
**Implements:** [IInterfaceSimpleComments](Test.IInterfaceSimpleComments.md), [Filler.IInterfaceB](Test.Filler.IInterfaceB.md)  

Tests class inheritance.  

# Properties

## PropertyA

**int { public get; public set; }**  

[Summary Tag] [Short] [One Line] Donec non interdum elit, a tincidunt massa.  

**Value:**  
[Value Tag] [Short] [One Line] Etiam vel quam vehicula, semper nulla vel, condimentum augue.  

**Remarks:**  
[Remarks Tag] [Long] [One Line] Vivamus at turpis vestibulum, ultricies nibh at, finibus tortor.  

**Misc:**  
[Floating Comment] [Short] [One Line] Ut massa nibh, rutrum nec urna in, interdum congue justo.  

**Example A:**  
[Example Tag] [Short] [One Line] Nulla ullamcorper est nec ipsum vehicula, eget tincidunt ante accumsan.  

**Permission: IInterfaceSimpleComments.PropertyA**  
[Permission Tag] [References Property] [Short] [One Line] Cras id egestas justo, a dapibus enim.  

## [IInterfaceSimpleComments](Test.IInterfaceSimpleComments.md).PropertyB

**int { private get; private set; }**  

**Permission: current member**  
[Permission Tag] [Explcit Interface Implementation Property]  

## [Filler.IInterfaceB](Test.Filler.IInterfaceB.md).PropertyB

**int { private get; private set; }**  

B: Enable test inheriting from multiple interfaces with the same member names.  

# Methods

## MethodA()

**void**  

[Summary Tag] [Short] [One Line] Pellentesque eros est, aliquet non nulla et, porttitor pharetra ligula.  

**Remarks:**  
[Remarks Tag] [Short] [One Line] Nullam in quam vel metus faucibus pulvinar.  

**Misc:**  
[Floating Comment] [Short] [One Line] Sed vehicula gravida efficitur.  

**Example A:**  
[Example Tag] [Short] [One Line] Vivamus eu mauris cursus, facilisis lectus sit amet, viverra neque.  

**Permission: IInterfaceSimpleComments.MethodA()**  
[Permission Tag] [References Method] [Short] [One Line] Nam justo elit, sagittis ac elementum sed, feugiat id ipsum.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: [Exception Tag] [Short] [One Line] Donec bibendum mauris finibus magna venenatis rutrum.  

## [Test.IInterfaceSimpleComments](Test.IInterfaceSimpleComments.md).MethodB()

**void**  

**Permission: current member**  
[Permission Tag] [Explcit Interface Implementation Method]  

## [Test.Filler.IInterfaceB](Test.Filler.IInterfaceB.md).MethodB()

**void**  

B: Enable test inheriting from multiple interfaces with the same member names.  

## MethodVirtual()

**virtual int**  

[Summary Tag] [Short] [One Line] Nunc eu egestas neque, a rutrum nunc.  

**Remarks:**  
[Remarks Tag] [Short] [One Line] Mauris finibus eros urna, in gravida metus ullamcorper at.  

**Misc:**  
[Floating Comment] [Short] [One Line] Curabitur euismod condimentum risus, ut pellentesque tortor fringilla in.  

**Example A:**  
[Example Tag] [Short] [One Line] Vivamus ut risus et nisl blandit cursus nec eu odio.  

**Permission: ClassSimpleComments.MethodVirtual()**  
[Permission Tag] [References Method] [Short] [One Line] Etiam augue enim, pharetra sit amet dictum sit amet, interdum in magna.  

**Exceptions:**  
* **[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)**: [Exception Tag] [Short] [One Line] Vestibulum turpis leo, gravida convallis dapibus at, feugiat ac est.  

