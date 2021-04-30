FROM node:lts-alpine AS publish
WORKDIR /app
COPY package*.json /app/
RUN npm install

COPY ./ /app/
RUN npm run build

FROM nginx:stable-alpine
WORKDIR /app
COPY --from=publish /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
