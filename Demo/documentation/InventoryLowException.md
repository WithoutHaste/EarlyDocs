# InventoryLowException

Inheritance: System.Object → System.Exception  
Implements: System.Runtime.Serialization.ISerializable, System.Runtime.InteropServices._Exception  
  
Exception related to an inventory quantity being too low.

## Properties

### ActualQuantity int

### ExpectedQuantity int

### Item Demo.Items.Item

The inventory item with low quantity.

## Constructors

### InventoryLowException(Demo.Items.Item item, int expectedQuantity, int actualQuantity)

