import { dirname, resolve } from "node:path";
import { fileURLToPath } from "node:url";
import { defineConfig } from "vite";
import tailwindcss from "@tailwindcss/vite";

const __dirname = dirname(fileURLToPath(import.meta.url));

// https://vite.dev/config/
export default defineConfig({
  plugins: [tailwindcss()],
  build: {
    manifest: true,
    outDir: resolve(__dirname, "wwwroot", "node__dist"),
    server: {
      open: "src_node/main.js",
    },
    lib: {
      entry: resolve(__dirname, "src_node/main.js"),
      name: "ClientSideCode",
      fileName: "client_side_code",
      formats: ["es"],
    },
  },
});
