@RunMe
Feature: e-commerce Shopping

To shop on the e-commerce site, the registered users want to make purchases, apply valid discount codes and go through the checkout process.

Background: 
Given I have logged in as a registered user
And I have cleared the cart

@DiscountOrder
Scenario Outline: Applying a discount code to a clothing item
	Given I have added a '<clothing item>' to cart 
	When I apply a valid coupon 'edgewords'
	Then the coupon takes off '15'%
	And the total is correct

	Examples: 
	| clothing item      |
	| Belt               |
	| Hoodie with Pocket |
	| Sunglasses         |

@CaptureOrder
Scenario: Completing the checkout process
    Given I have added a 'Beanie' to cart
	And I have proceeded to checkout
	And I provide the billing details
	| first name | last name | address         | city        | postcode | phone number |
	| Layla      | Patel     | 42 Rupert St    | London      | W1D 6DP  | 02072876333  |
	When I place an order
	Then that same order is displayed in my account