export default {
  rules: {
    password(p1, p2) {
      return [
        () => p1 === p2 || "Różne hasła",
        (v) => v.length > 1 || "Długość hasła",
      ];
    },
  },
};
