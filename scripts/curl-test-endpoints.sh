#!/bin/bash
BASE_URL="http://localhost:5007"
echo "Testing with BASE_URL: $BASE_URL"

printf "\nTest 1\n---===---\n"
printf "  actual: "
curl $BASE_URL
printf "\nexpected: Hello, world!\n"

printf "\nTest 2\n---===---\n"
printf "  actual: "
curl $BASE_URL/recipe/ingredients
printf '\nexpected: [{"id":1,"name":"Bread Flour","mass":1000},{"id":2,"name":"Water","mass":700},{"id":3,"name":"Salt","mass":20},{"id":4,"name":"Starter","mass":200}]\n'

printf "\nTest 3\n---===---\n"
printf "  actual: "
curl $BASE_URL/recipe/ingredients/1
printf '\nexpected: {"id":1,"name":"Bread Flour","mass":1000}\n'

printf "\nTest 4\n---===---\n"
printf "  actual: "
curl $BASE_URL/recipe/ingredients/3
printf '\nexpected: {"id":3,"name":"Salt","mass":20}\n'

printf "\nTest 5\n---===---\n"
printf "  actual: "
curl $BASE_URL/recipe/ingredients/4
printf '\nexpected: {"id":4,"name":"Starter","mass":200}\n'

printf "\nTest 6\n---===---\n"
printf "  actual: "
curl $BASE_URL/recipe/ingredients/5
printf '\nexpected: {"error":"Ingredient not found"}\n'

