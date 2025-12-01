# Build step
FROM node:20 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Serve step (static hosting)
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html

# Allow the frontend to reach the API
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
