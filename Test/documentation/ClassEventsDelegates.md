# ClassEventsDelegates

**Inheritance:** object  
  
Tests events and delegates.  
  

# Fields

## Action<int> ActionInClass

[Summary Tag] [Short] [One Line] Suspendisse mattis lacinia orci, eu cursus lacus ultricies ac.  
  
**Remarks:**  
[Remarks Tag] [Short] [One Line] Donec dictum consectetur lacus non sodales.  
  
**Example:**  
[Example Tag] [Short] [One Line] Aenean mi leo, sagittis a mollis vel, pretium eu justo. In auctor est eget nibh luctus blandit.  
  
**Permission:**  
[Permission Tag] [References Action] [Short] [One Line] Quisque egestas ante nec feugiat lacinia.  
  
[Floating Comment] [Short] [One Line] Donec gravida egestas nibh.  

## Func<int,string> FuncInClass

[Summary Tag] [Short] [One Line] Maecenas et turpis eget mi dapibus sodales.  
  
**Remarks:**  
[Remarks Tag] [Short] [One Line] Phasellus id pharetra justo.  
  
**Example:**  
[Example Tag] [Short] [One Line] In commodo, arcu eget vulputate faucibus, justo libero varius nulla, sed tempus nisl nibh in augue.  
  
**Permission:**  
[Permission Tag] [References Func] [Short] [One Line] Quisque neque sapien, vulputate commodo leo non, mollis suscipit nunc.  
  
[Floating Comment] [Short] [One Line] Nunc bibendum, purus sit amet sodales molestie, libero metus mattis massa, porttitor vehicula sem ligula in quam.  

# Events

## Test.EventHandlerGlobal EventA

[Summary Tag] [Short] [One Line] Mauris sit amet vestibulum justo.  
  
**Remarks:**  
[Remarks Tag] [Short] [One Line] Praesent volutpat nibh vehicula rutrum condimentum.  
  
**Example:**  
[Example Tag] [Short] [One Line] Nulla placerat ultricies augue, mollis rutrum tellus tempus ut.  
  
**Permission:**  
[Permission Tag] [References Event] [Short] [One Line] Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.  
  
[Floating Comment] [Short] [One Line] Suspendisse sodales sem ut massa gravida aliquet.  

## Test.ClassEventsDelegates.EventHandlerInClass EventB

[Summary Tag] [Short] [One Line] Suspendisse non enim semper sapien volutpat rhoncus.  
  
**Remarks:**  
[Remarks Tag] [Short] [One Line] Fusce vel quam ac urna sagittis suscipit non sit amet nulla.  
  
**Example:**  
[Example Tag] [Short] [One Line] Vestibulum tincidunt justo ut lorem consectetur, a facilisis tortor lacinia.  
  
**Permission: EventA**  
[Permission Tag] [References Event] [Short] [One Line] Quisque egestas ante nec feugiat lacinia.  
  
[Floating Comment] [Short] [One Line] Nunc faucibus commodo ultrices.  

## EventHandler EventC

[Summary Tag] [Private Member] [Should Ignore] Duis justo dolor, dapibus eu pulvinar vitae, iaculis non neque.  
  

# Methods

## void MethodWithAction(Action<int> action)

[Summary Tag] [Short] [One Line] Integer bibendum, orci ut blandit scelerisque, justo nibh fermentum mi, efficitur malesuada neque velit id ipsum.  
  
**Remarks:**  
[Remarks Tag] [Short] [One Line] Phasellus ut nulla aliquam, commodo ante ac, accumsan leo.  
  

### Parameters

#### action

[Parameter Tag] [Short] [One Line] Suspendisse fermentum suscipit purus, sodales dapibus ex faucibus et.  

  
**Example:**  
[Example Tag] [Short] [One Line] Duis metus ex, suscipit quis quam vel, viverra imperdiet magna.  
  
**Permission:**  
[Permission Tag] [References Method] [Short] [One Line] Nam vitae risus quis est aliquet egestas.  
  
**Exceptions:**  
_ArgumentException:_ [Exception Tag] [Short] [One Line] Curabitur dolor nisi, porta eget est sed, lobortis venenatis ligula.  
  
[Floating Comment] [Short] [One Line] Vestibulum porttitor consectetur pellentesque.  

## void MethodWithFunc(Func<int,string> func)

[Summary Tag] [Short] [One Line] Donec accumsan lacus a dolor auctor, sit amet cursus eros faucibus.  
  
**Remarks:**  
[Remarks Tag] [Short] [One Line] Pellentesque est nibh, vehicula at velit id, scelerisque pretium tortor.  
  

### Parameters

#### func

[Parameter Tag] [Short] [One Line] Aenean quis mi id leo scelerisque euismod eget vel lacus.  

  
**Example:**  
[Example Tag] [Short] [One Line] Vivamus lobortis neque eget mi semper finibus.  
  
**Permission:**  
[Permission Tag] [References Method] [Short] [One Line] In aliquet libero in euismod rutrum.  
  
**Exceptions:**  
_ArgumentException:_ [Exception Tag] [Short] [One Line] Sed fermentum ornare nisi, et vestibulum lorem tincidunt in.  
  
[Floating Comment] [Short] [One Line] In a sodales tortor, quis mollis augue.  

