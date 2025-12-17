import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
    server: {
        proxy: {
            '/test': {
                target: 'http://localhost:8080',
                changeOrigin: true,
            }
        }
    },
    plugins: [react()],
    build: {
        outDir: "dist",
    }
});
