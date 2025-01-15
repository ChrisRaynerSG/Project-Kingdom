# Re-importing necessary libraries and re-defining the logic due to environment reset.
from itertools import combinations

# Define the range of numbers and the inclusion/exclusion criteria
numbers = list(range(8))
must_include = {3,5,6,7}
must_exclude = {2,4}

# Generate all valid combinations with the updated criteria
valid_combinations = []
for r in range(len(numbers) + 1):
    for combo in combinations(numbers, r):
        combo_set = set(combo)
        if must_include.issubset(combo_set) and must_exclude.isdisjoint(combo_set):
            valid_combinations.append(combo)

valid_combinations
for combo in valid_combinations:
    print(combo)