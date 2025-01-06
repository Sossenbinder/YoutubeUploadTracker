import {
  Group,
  RenderTreeNodePayload,
  Tree,
  TreeNodeData,
  useTree,
  getTreeExpandedState,
  Text,
} from "@mantine/core";
import {
  IconBrandYoutubeFilled,
  IconMinus,
  IconPlus,
} from "@tabler/icons-react";
import styles from "./SideBar.module.scss";
import { navigate } from "vike/client/router";
import { usePageContext } from "vike-react/usePageContext";
import classNames from "classnames";

const navigationTreeData: TreeNodeData[] = [
  {
    label: "Channels",
    value: "/channels",
    nodeProps: {
      icon: <IconBrandYoutubeFilled size={20} style={{ color: "#e30000" }} />,
    },
    children: [
      {
        label: "Mine",
        value: "/channels/mine",
      },
      {
        label: "Trending",
        value: "/channels/trending",
      },
      {
        label: "Popular",
        value: "/channels/popular",
      },
    ],
  },
];

const NavigationTreeLeaf = ({
  elementProps,
  node,
  expanded,
  hasChildren,
  urlPathname,
}: RenderTreeNodePayload & { urlPathname: string }) => {
  const isActivePath = urlPathname === node.value;

  if (hasChildren) {
    return (
      <div
        {...elementProps}
        className={classNames(
          "flex items-center flex-row gap-2 justify-between",
          { [styles.activePath]: isActivePath }
        )}>
        <div className="flex items-center flex-row gap-2">
          {node.nodeProps?.["icon"]}
          <a href={node.value}>{node.label}</a>
        </div>
        {expanded ? <IconMinus size={24} /> : <IconPlus size={24} />}
      </div>
    );
  }

  return (
    <div
      {...elementProps}
      className={classNames(elementProps.className, {
        [styles.activePath]: isActivePath,
      })}
      onClick={() => navigate(node.value)}>
      <Text>{node.label as string}</Text>
    </div>
  );
};

export default function Menubar() {
  const tree = useTree({
    initialExpandedState: getTreeExpandedState(navigationTreeData, "*"),
  });

  const { urlPathname } = usePageContext();

  return (
    <div className="flex flex-col gap-8">
      <h1 className="text-xl font-bold">Navigation</h1>
      <div className="flex flex-col gap-4">
        <Tree
          classNames={{
            subtree: styles["tree-subtree"],
          }}
          data={navigationTreeData}
          tree={tree}
          levelOffset={"xl"}
          renderNode={(x) => (
            <NavigationTreeLeaf {...x} urlPathname={urlPathname} />
          )}
        />
      </div>
    </div>
  );
}
