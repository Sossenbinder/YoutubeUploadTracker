import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";
import vike from "vike/plugin";
import path from "path";

export default defineConfig({
  resolve: {
    alias: {
      "@root": __dirname,
      "@components": path.resolve(__dirname, "./components"),
      "@common": path.resolve(__dirname, "./common"),
      "@pages": path.resolve(__dirname, "./pages"),
    },
    extensions: [".js", ".ts", ".jsx", ".tsx"], // Include '.tsx' here
  },
  plugins: [vike({}), react({})],
});
