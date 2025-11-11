# Supermarket Receipt test-list

## Product
- [x] Have a name and a price
- [x] Allow decimal prices
- [x] Throw an exception when price is negative

## Product quantity
- [x] Store product and quantity
- [x] Throw an exception when quantity is zero or negative

## Shopping cart
- [x] Be empty when created
- [x] Add a single product
- [x] Add multiple different products
- [x] Accumulate quantity when adding the same product
- [x] Return total price without discounts

## Receipt
- [ ] Contain all cart items
- [ ] Calculate subtotal correctly
- [ ] Show zero total when cart is empty
- [ ] Include total price
- [ ] List items in the order they were added

## Percentage Discount

-[ ] TenPercentDiscount_ShouldApplyToSingleProduct
-[ ] PercentageDiscount_ShouldReducePriceCorrectly_ForMultipleQuantities
-[ ] PercentageDiscount_ShouldNotAffectOtherProducts

### “3 for 2” Offer

-[ ] ThreeForTwoOffer_ShouldDiscountOne_WhenBuyingThree
-[ ] ThreeForTwoOffer_ShouldApplyMultipleTimes_WhenBuyingSix
-[ ] ThreeForTwoOffer_ShouldNotApply_WhenBuyingLessThanThree

### “X for Y” Offer (general)

-[ ] XForYOffer_ShouldApplyCorrectly_WhenBuyingExactMultiple
-[ ] XForYOffer_ShouldApplyCorrectly_WhenBuyingMoreThanMultiple
-[ ] XForYOffer_ShouldIgnoreRemainderItems

### Bulk Discount

-[ ] BulkDiscount_ShouldApply_WhenBuyingOverThreshold
-[ ] BulkDiscount_ShouldNotApply_WhenBelowThreshold

### ReceiptPrinter

-[ ] ReceiptPrinter_ShouldPrintHeaderAndFooter
-[ ] ReceiptPrinter_ShouldPrintEachItemWithQuantityAndPrice

-[ ] ReceiptPrinter_ShouldPrintDiscountLines_WhenApplicable
-[ ] ReceiptPrinter_ShouldPrintTotalAmount

## Special validations

-[ ] ShoppingCart_ShouldHandleFloatingPointPricesAccurately
-[ ] Receipt_ShouldRoundToTwoDecimals
-[ ] Offer_ShouldHandleZeroPriceProductsGracefully
-[ ] ReceiptPrinter_ShouldHandleEmptyCart

## Percentage discount
- [ ] Apply ten percent discount to a single product
- [ ] Reduce price correctly for multiple quantities
- [ ] Not affect other products

## “3 for 2” offer
- [ ] Discount one item when buying three
- [ ] Apply multiple times when buying six
- [ ] Not apply when buying less than three

## “X for Y” offer (general)
- [ ] Apply correctly when buying an exact multiple
- [ ] Apply correctly when buying more than a multiple
- [ ] Ignore remainder items

## Bulk discount
- [ ] Apply when buying over threshold
- [ ] Not apply when below threshold

## Receipt printer
- [ ] Print header and footer
- [ ] Print each item with quantity and price
- [ ] Print discount lines when applicable
- [ ] Print total amount

## Special validations
- [ ] Handle floating point prices accurately
- [ ] Round totals to two decimals
- [ ] Handle zero price products gracefully
- [ ] Handle empty cart