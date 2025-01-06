import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";
import vike from "vike/plugin";
import path from "path";

export default defineConfig(({ mode }) => {
  if (mode === "development") {
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
  }

  return {
    resolve: {
      alias: {
        "@root": __dirname,
        "@features": path.resolve(__dirname, "./src/features"),
        "@auth": path.resolve(__dirname, "./src/features/auth"),
        "@common": path.resolve(__dirname, "./src/common"),
        "@pages": path.resolve(__dirname, "./src/pages"),
      },
    },
    plugins: [react(), vike()],
    server: {
      host: "0.0.0.0",
    },
    css: {
      preprocessorOptions: {
        scss: {
          api: "modern-compiler",
          additionalData: `@use "${path
            .join(process.cwd(), "src/_mantine")
            .replace(/\\/g, "/")}" as mantine;`,
        },
      },
    },
  };
});
