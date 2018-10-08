# Item

**Abstract**  
**Inheritance:** System.Object  
  
Represents an item that can be held in inventory.

## Fields

### Constant Fields

#### NO_MAX_QUANTITY int

Indicates item has no limit on inventory quantity.

## Properties

### LongDescription string

### MaxQuantity int

The maximum number of this item that can be held in inventory at one time.

### Name string

### ShortDescription string

## Constructors

### Item(string name, string shortDescription, string longDescription, int maxQuantity)

#### Parameters

##### name

Display name of item.

##### shortDescription

Short description of item. Limit to 256 characters.

##### longDescription

Long description of item. No length limit.

##### maxQuantity

Maximum number of units that can be carried in inventory at one time.
