@RunMe
Feature: e-commerce Shopping

To shop on the e-commerce site, the registered users want to make purchases, apply valid discount codes and go through the checkout process.

Background: 
Given I have dismissed the notice

@DiscountOrder
Scenario Outline: Applying a discount code to a clothing item
	Given I have added a '<clothing item>' to cart 
	When I apply a valid coupon 'edgewords'
	Then the coupon takes off 15%

	Examples: 
	| clothing item      |
	| Belt               |
	| Hoodie with Pocket |
	| Sunglasses         |

@CaptureOrder
Scenario: Completing the checkout process
	Given I have proceeded to checkout
	And I provide the billing details
	When I place an order
	Then that same order is displayed in my account

