import vikeReact from "vike-react/config";
import { Config } from "vike/types";
import Layout from "./Layout";
import Root from "../features/Root";

// Default config (can be overridden by pages)
// https://vike.dev/config

const config: Config = {
  // https://vike.dev/Layout
  Layout,

  // https://vike.dev/head-tags
  title: "Youtube Upload Tracker",
  Wrapper: Root,
  extends: vikeReact,
};

export default config;
