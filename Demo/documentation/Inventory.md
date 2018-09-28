# Inventory

Base type: System.Object.

Represents a character's inventory.

## Constructors

### Inventory()



## Methods

### Void Decrement(WithoutHaste.DataFiles.Markdown.MarkdownText, WithoutHaste.DataFiles.Markdown.MarkdownText)

EarlyDocs.XmlComments

Parameter quantity: Amount to remove from inventory.  

ArgumentException:  cannot be less than 1.
InventoryLowException: There are not enough units of  in inventory.



### Void Increment(WithoutHaste.DataFiles.Markdown.MarkdownText, WithoutHaste.DataFiles.Markdown.MarkdownText)

EarlyDocs.XmlComments

Parameter quantity: Amount to add to inventory.  

ArgumentException:  cannot be less than 1.



