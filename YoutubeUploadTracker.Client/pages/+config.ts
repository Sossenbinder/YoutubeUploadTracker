import vikeReact from "vike-react/config";
import { Config } from "vike/types";
import Layout from "../common/Layout";
import Root from "../common/Root";

// Default config (can be overridden by pages)
// https://vike.dev/config

export default {
  // https://vike.dev/Layout
  Layout,

  // https://vike.dev/head-tags
  title: "Youtube Upload Tracker",
  Wrapper: Root,
  extends: vikeReact,
} satisfies Config;
