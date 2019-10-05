Attrubutes specific for commerce
================================

Here are a list over special attrubutes used by Episerver Commerce

CatalogContentType
------------------

.. code-block:: csharp

 [CatalogContentType(DisplayName = "BlouseProduct", MetaClassName = "blouse_product", GUID = "FEDA29DF-020F-4170-B3B5-6E93BAA0A30D", Description = "")]
 public class BlouseProduct : ProductContent

Encrypted
---------
Will encrypt the value stored by Meta data plus
(windows + SQL)

UseInComparison
---------------
 ???

IncludeInDefaultSearch
----------------------
IncludeValuesInSearchResults
----------------------------
SortableInSearchResults
-----------------------
Tokenize
--------
Use this attribute if values of a property should be broken down in individual searchable words.

DecimalSettings
---------------

Attribute is used for being able to set precision and scale.