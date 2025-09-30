import { validateRIB } from "./index";

const tests = [
  "00100234567890123456", // RIB de test
];

for (const t of tests) {
  console.log(t, "=>", validateRIB(t));
}