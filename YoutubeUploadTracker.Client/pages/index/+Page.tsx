import React from "react";
import { Counter } from "./Counter.js";

export async function data() {
  console.log("Hello world");
  return {
    nr: 5,
  };
}

export default function Page() {
  return (
    <>
      <h1>Youtube Upload Tracker</h1>
    </>
  );
}
