import { defineConfig } from "astro/config";

export default defineConfig({
  outDir: "./docs",
  site: "https://bartlomiej-aleksiejczyk.github.io",
  base: `/file-based-media-engine/`,
  srcDir: "src_node",
});
