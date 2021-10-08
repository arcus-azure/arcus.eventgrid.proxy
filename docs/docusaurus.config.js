const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');

/** @type {import('@docusaurus/types').DocusaurusConfig} */
module.exports = {
  title: 'Arcus - Event Grid Proxy',
  url: 'https://eventgrid-proxy.arcus-azure.net',
  baseUrl: '/',
  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',
  favicon: 'img/arcus.png',
  organizationName: 'arcus-azure', // Usually your GitHub org/user name.
  projectName: 'Arcus - Event Grid Proxy', // Usually your repo name.
  themeConfig: {
    algolia: {
      apiKey: 'fdbac718aeb246ac60aeae5bd286cbbf',
      indexName: 'arcus-azure',
      // Set `contextualSearch` to `true` when having multiple versions!!!
      contextualSearch: false,
      searchParameters: {
        facetFilters: ["tags:eventgrid-proxy"]
      },
    },
    image: 'img/arcus.jpg',
    navbar: {
      title: 'Event Grid Proxy',
      logo: {
        alt: 'Arcus',
        src: 'img/arcus.png',
        srcDark: 'img/arcus.png'
      },
      items: [
        // Uncomment when having multiple versions
        // {
        //   type: 'docsVersionDropdown',
        //
        //   //// Optional
        //   position: 'right',
        //   // Add additional dropdown items at the beginning/end of the dropdown.
        //   dropdownItemsBefore: [],
        //   // Do not add the link active class when browsing docs.
        //   dropdownActiveClassDisabled: true,
        //   docsPluginId: 'default',
        // },
        {
          type: 'search',
          position: 'right',
        },
        {
          href: 'https://github.com/arcus-azure/arcus.eventgrid.proxy',
          label: 'GitHub',
          position: 'right',
        },
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Community',
          items: [
            {
              label: 'Arcus Azure Github',
              href: 'https://github.com/arcus-azure',
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()}, Arcus - Event Grid Proxy maintained by arcus-azure`,
    },
    prism: {
      theme: lightCodeTheme,
      darkTheme: darkCodeTheme,
      additionalLanguages: ['csharp', 'powershell'],
    },
  },
  presets: [
    [
      '@docusaurus/preset-classic',
      {
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
          routeBasePath: "/",
          path: 'preview',
          sidebarCollapsible: false,
          // Please change this to your repo.
          editUrl:
            'https://github.com/arcus-azure/arcus.eventgrid.proxy/edit/master/docs',
          // includeCurrentVersion:process.env.CONTEXT !== 'production',

        },
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      },
    ],
  ],
};
