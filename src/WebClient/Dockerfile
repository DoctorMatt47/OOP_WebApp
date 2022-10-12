FROM node:18-alpine AS build
WORKDIR /app
COPY ["src/WebClient/package.json", "./package.json"]
RUN npm install
COPY src/WebClient . 
RUN npm run build

FROM nginx:1.17.1-alpine
EXPOSE 80
COPY src/WebClient/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/web-client /usr/share/nginx/html
