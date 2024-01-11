<?php

/**
 * Class RestaurantPOS
 *
 * This class represents a point of sale system for a restaurant with stock management and inventory.
 */
class RestaurantPOS {

    /** @var array $inventory The inventory of the restaurant. */
    private $inventory;

    /**
     * Constructor for the RestaurantPOS class.
     *
     * @param array $inventory The initial inventory of the restaurant.
     */
    public function __construct($inventory) {
        $this->inventory = $inventory;
    }

    /**
     * Add a new item to the inventory.
     *
     * @param string $item The name of the item.
     * @param int $quantity The quantity of the item.
     */
    public function addItem($item, $quantity) {
        if (isset($this->inventory[$item])) {
            $this->inventory[$item] += $quantity;
        } else {
            $this->inventory[$item] = $quantity;
        }
    }

    /**
     * Remove an item from the inventory.
     *
     * @param string $item The name of the item.
     * @param int $quantity The quantity of the item to be removed.
     *
     * @throws Exception If the item does not exist in the inventory or the quantity is greater than the available quantity.
     */
    public function removeItem($item, $quantity) {
        if (isset($this->inventory[$item])) {
            if ($this->inventory[$item] >= $quantity) {
                $this->inventory[$item] -= $quantity;
            } else {
                throw new Exception("Insufficient quantity of {$item} in the inventory.");
            }
        } else {
            throw new Exception("{$item} does not exist in the inventory.");
        }
    }

    /**
     * Get the current quantity of an item in the inventory.
     *
     * @param string $item The name of the item.
     *
     * @return int The current quantity of the item in the inventory.
     *
     * @throws Exception If the item does not exist in the inventory.
     */
    public function getQuantity($item) {
        if (isset($this->inventory[$item])) {
            return $this->inventory[$item];
        } else {
            throw new Exception("{$item} does not exist in the inventory.");
        }
    }

    /**
     * Process an order by updating the inventory.
     *
     * @param array $order The order details, including the items and quantities.
     *
     * @throws Exception If any item in the order does not exist in the inventory or the quantity is greater than the available quantity.
     */
    public function processOrder($order) {
        foreach ($order as $item => $quantity) {
            $this->removeItem($item, $quantity);
        }
    }

    /**
     * Get the current inventory of the restaurant.
     *
     * @return array The current inventory of the restaurant.
     */
    public function getInventory() {
        return $this->inventory;
    }

    /**
     * Integrate with an API to update the inventory.
     *
     * @param string $apiEndpoint The API endpoint to update the inventory.
     *
     * @return bool Whether the integration was successful or not.
     */
    public function integrateWithAPI($apiEndpoint) {
        // Code to integrate with the API and update the inventory
        // Return true if successful, false otherwise
        // ...
    }
}

?>

<?php

// Usage demonstration of the RestaurantPOS class

// Example 1: Initializing the point of sale system with an initial inventory
$initialInventory = [
    'item1' => 10,
    'item2' => 5,
    'item3' => 8
];
$pos = new RestaurantPOS($initialInventory);

// Example 2: Adding new items to the inventory
$pos->addItem('item4', 15);
$pos->addItem('item5', 20);

// Example 3: Removing items from the inventory
$pos->removeItem('item2', 2);

// Example 4: Getting the current quantity of an item in the inventory
$itemQuantity = $pos->getQuantity('item3');
echo "The current quantity of item3 is {$itemQuantity}.\n";

// Example 5: Processing an order and updating the inventory
$order = [
    'item1' => 2,
    'item3' => 4
];
$pos->processOrder($order);

// Example 6: Getting the current inventory
$currentInventory = $pos->getInventory();
echo "Current inventory:\n";
foreach ($currentInventory as $item => $quantity) {
    echo "{$item}: {$quantity}\n";
}

// Example 7: Integrating with an API to update the inventory
$apiEndpoint = 'https://example.com/api/update-inventory';
$integrationResult = $pos->integrateWithAPI($apiEndpoint);
if ($integrationResult) {
    echo "Integration with API successful.\n";
} else {
    echo "Integration with API failed.\n";
}

?>
